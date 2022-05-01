CREATE TABLE "project" (
  "id" int4 NOT NULL,
  "name" varchar(255),
  "start_date" date,
  "completion_date" date,
  "status" int2,
  "priority" int4,
  PRIMARY KEY ("id")
);

CREATE TABLE "task" (
  "id" int4 NOT NULL,
  "name" varchar(255),
  "task_status" int2,
  "description" text,
  "priority" int2,
  "completion_date" date,
  "project_id" int4,
  PRIMARY KEY ("id")
);

ALTER TABLE "task" ADD CONSTRAINT "fk_task_project_1" FOREIGN KEY ("project_id") REFERENCES "project" ("id");

