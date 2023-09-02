CREATE TABLE users (
	id uuid NOT NULL,
	email varchar(100) NOT NULL,
	"password" varchar(100) NOT NULL,
	"name" varchar(100) NOT NULL,
	created_at timestamp NOT NULL,
	CONSTRAINT users_pk PRIMARY KEY (id),
	CONSTRAINT users_un UNIQUE (email)
);

INSERT INTO users (id, email, password, name, created_at) 
VALUES('3d49879f-5901-4f8a-af9b-1cf3bc865184', 'test@test.com', 'b642b4217b34b1e8d3bd915fc65c4452', 'Test Name', '2023-09-01 15:07:29.906');

