-- =====================================================================
-- Школьное расписание — таблицы базы.
--
-- Программа создаёт всё это сама при первом запуске. Файл нужен на тот
-- случай, когда у пользователя базы нет прав на CREATE (частая история
-- на школьном сервере): тогда таблицы заводит администратор, а программа
-- находит всё готовым.
--
-- Как загрузить: phpMyAdmin → «Создать базу данных» → school_schedule
-- (сравнение utf8mb4_unicode_ci) → выбрать её слева → вкладка «SQL» →
-- вставить этот текст → «Вперёд».
-- =====================================================================

CREATE DATABASE IF NOT EXISTS `school_schedule`
  DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;

USE `school_schedule`;


-- ── Классы ───────────────────────────────────────────────────────────
-- Заранее их нет: классы заводит учитель в самой программе. Порядок
-- (sort_order) задаётся кнопками «Выше»/«Ниже» и определяет, в каком
-- порядке классы идут на экране.

CREATE TABLE IF NOT EXISTS classes (
  id         INT         NOT NULL AUTO_INCREMENT,
  name       VARCHAR(32) NOT NULL,
  sort_order INT         NOT NULL DEFAULT 0,
  PRIMARY KEY (id),
  UNIQUE KEY uq_classes_name (name)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;


-- ── Звонки ───────────────────────────────────────────────────────────
-- По ним на экране подсвечивается идущий урок.

CREATE TABLE IF NOT EXISTS lesson_times (
  lesson_no  TINYINT NOT NULL,
  start_time TIME    NOT NULL,
  end_time   TIME    NOT NULL,
  PRIMARY KEY (lesson_no)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;


-- ── Расписание ───────────────────────────────────────────────────────
-- Обе сетки лежат в одной таблице и различаются полем variant:
--     0 — обычное (постоянное) расписание,
--     1 — изменённое (заболел учитель, актовый день и т. п.).
--
-- Так сделано нарочно: переключение показа — это смена одного числа в
-- settings, а не подмена таблиц. И запросы для обеих сеток одинаковые.
--
-- uq_cell не даёт положить два урока в одну клетку.
--
-- Внешнего ключа на classes здесь нет намеренно. Для его создания MySQL
-- требует право REFERENCES, а школьному пользователю базы его выдают далеко
-- не всегда — на такой учётной записи установка падала бы с ошибкой
-- «REFERENCES command denied». Уроки удалённого класса программа убирает
-- сама, одной транзакцией.

CREATE TABLE IF NOT EXISTS schedule (
  id        INT          NOT NULL AUTO_INCREMENT,
  variant   TINYINT      NOT NULL,
  class_id  INT          NOT NULL,
  weekday   TINYINT      NOT NULL,   -- 1 = понедельник … 7 = воскресенье
  lesson_no TINYINT      NOT NULL,
  subject   VARCHAR(120) NOT NULL,
  teacher   VARCHAR(120) NULL,
  room      VARCHAR(32)  NULL,
  PRIMARY KEY (id),
  UNIQUE KEY uq_cell (variant, class_id, weekday, lesson_no),
  KEY idx_day (variant, weekday, class_id, lesson_no),
  KEY idx_class (class_id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;


-- ── Календарь ────────────────────────────────────────────────────────
-- Праздники, каникулы и дни, которым назначена своя сетка.
--   is_holiday = 1        — уроков нет, на экране вместо таблицы надпись;
--   variant IS NOT NULL   — в этот день показывать именно эту сетку,
--                           не трогая общий переключатель.
--
-- Второе — это способ поставить изменённое расписание «только на завтра».

CREATE TABLE IF NOT EXISTS calendar_days (
  day        DATE         NOT NULL,
  is_holiday TINYINT(1)   NOT NULL DEFAULT 0,
  title      VARCHAR(120) NULL,
  variant    TINYINT      NULL,
  note       VARCHAR(255) NULL,
  PRIMARY KEY (day)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;


-- ── Настройки ────────────────────────────────────────────────────────
-- Что именно висит на экране. Программа на телевизоре читает эту таблицу
-- и слушается её, а учитель меняет значения из окна редактора.
--
-- Отдельно стоит строка revision: это счётчик правок. Телевизор раз в
-- несколько секунд спрашивает только его — один крошечный запрос — и
-- перечитывает расписание, лишь когда число выросло. Из-за этого замена
-- доезжает до экрана сама, без перезапуска программы.

CREATE TABLE IF NOT EXISTS settings (
  k VARCHAR(64)  NOT NULL,
  v VARCHAR(255) NOT NULL,
  PRIMARY KEY (k)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;


-- ── Значения по умолчанию ────────────────────────────────────────────
-- INSERT IGNORE: уже выставленное учителем повторный запуск не тронет.

INSERT IGNORE INTO settings (k, v) VALUES
  ('revision',          '1'),                  -- счётчик правок
  ('active_variant',    '0'),                  -- 0 обычное / 1 изменённое
  ('display_mode',      'day'),                -- day = все классы, week = неделя класса
  ('display_class_id',  '0'),
  ('display_date_mode', 'today'),              -- today | tomorrow | next | fixed
  ('display_date',      ''),
  ('school_name',       'Расписание уроков'),
  ('ticker',            ''),                   -- объявление внизу экрана
  ('lessons_count',     '8'),
  ('days_count',        '6'),                  -- 5 или 6 учебных дней
  ('auto_rotate',       '0'),
  ('rotate_seconds',    '20'),
  ('idle_seconds',      '120'),                -- возврат к показу по умолчанию
  ('tomorrow_after',    ''),                   -- ЧЧ:ММ — после этого часа показывать завтра
  ('classes_per_page',  '8'),
  ('show_replacements', '1'),
  ('theme',             'dark');            -- dark или light

-- Пароль учителя (settings.admin_password) здесь не заводится нарочно:
-- пока строки нет, действует пароль из ip.txt. Как только учитель сменит
-- его в программе, сюда ляжет хеш вида pbkdf2$100000$…$… — открытым
-- текстом пароль в базе не хранится.


-- ── Звонки по умолчанию ──────────────────────────────────────────────

INSERT IGNORE INTO lesson_times (lesson_no, start_time, end_time) VALUES
  (1, '08:30:00', '09:15:00'),
  (2, '09:25:00', '10:10:00'),
  (3, '10:25:00', '11:10:00'),
  (4, '11:25:00', '12:10:00'),
  (5, '12:20:00', '13:05:00'),
  (6, '13:15:00', '14:00:00'),
  (7, '14:10:00', '14:55:00'),
  (8, '15:05:00', '15:50:00');


-- ── Проверка ─────────────────────────────────────────────────────────
-- Должно вернуться 17 строк настроек и 8 звонков.

SELECT (SELECT COUNT(*) FROM settings)     AS настроек,
       (SELECT COUNT(*) FROM lesson_times) AS звонков,
       (SELECT COUNT(*) FROM classes)      AS классов;
