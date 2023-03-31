# REST API сервис мероприятий.

## Запуск сервиса
Проект запускается через docker-compose.

Для запуска требуется установленный docker с поддержкой Linux контейнеров.

Для запуска сервиса следует проделать следующие шаги:

Склонировать репозиторий:

Code -- Open with Visual Studio (если используете Visual Studio);

Зайти в проект;

Запустить с использованием Docker Compose

![image](https://user-images.githubusercontent.com/78857901/228610389-af4e89b2-1f03-4c0c-80ac-a4b8b76356c2.png)

При запуске открывается страница Swagger:

![image](https://user-images.githubusercontent.com/78857901/229216146-21d1951f-74dd-402c-a2a8-da6bbdf00fd9.png)

## Авторизация

Получение JWT токена происходит по результату выполнения данного Get запроса:

![image](https://user-images.githubusercontent.com/78857901/229216298-6545a1b3-4486-4122-a159-7ed7b149f0bd.png)

Пример Header'a для авторизации в Postman

![image](https://user-images.githubusercontent.com/78857901/227791870-e9245bdb-3396-406e-91a0-e893e7cfd7b9.png)

## Добавление мероприятия

Для того, чтобы добавить мероприятие, необходимо ввести Id пространства и при желании Id афиши.

GET запрос на получение Id пространств происходит по адресу:

http://localhost:7002/room

Get запрос на получение Id афиш происходит по адресу:

http://localhost:7003/image
