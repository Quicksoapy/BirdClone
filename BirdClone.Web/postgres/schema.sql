CREATE TABLE IF NOT EXISTS Messages (
    id serial PRIMARY KEY,
    user_id INT NOT NULL,
    content VARCHAR (1000) NOT NULL,
    created_on TIMESTAMP WITH TIME ZONE NOT NULL
);

CREATE TABLE IF NOT EXISTS Accounts (
    id serial PRIMARY KEY,
    username VARCHAR (50) UNIQUE NOT NULL,
    password VARCHAR (512) UNIQUE NOT NULL,
    email VARCHAR (255) UNIQUE,
    country VARCHAR (255) NOT NULL,
    created_on TIMESTAMP WITH TIME ZONE NOT NULL,
    last_login TIMESTAMP WITH TIME ZONE
);

CREATE TABLE IF NOT EXISTS Reposts (
    id serial PRIMARY KEY,
    message_id INT NOT NULL,
    user_id INT NOT NULL,
    created_on TIMESTAMP WITH TIME ZONE NOT NULL
);

