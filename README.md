# REST API сервис мероприятий.

## Запуск сервиса
Проект запускается через docker-compose.

Для запуска требуется установленный docker с поддержкой Linux контейнеров.

При запуске открывается страница Swagger:

![image](https://user-images.githubusercontent.com/78857901/227791797-e3fdfd63-92d1-4261-9188-695a7c0634e2.png)

Получение JWT токена происходит по результату выполнения данного Get запроса:

![image](https://user-images.githubusercontent.com/78857901/227717603-c8ea0e51-0846-4876-9bbc-c9d715c63f99.png)

Пример Header'a для авторизации в Postman

![image](https://user-images.githubusercontent.com/78857901/227791870-e9245bdb-3396-406e-91a0-e893e7cfd7b9.png)

## Добавление мероприятия

Для того, чтобы добавить мероприятие, необходимо ввести Id пространства и при желании Id афиши.

GET запрос на получение Id пространств происходит по адресу:

http://localhost:7002/room

Get запрос на получение Id афиш происходит по адресу:

http://localhost:7003/image
