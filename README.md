# TestWork1024
 Тестовое задание склады-ресурсы
 
Все скрипты писались с нуля (не делал ранее похожие проекты) так что возможно архитектура хромает. Старался по максимуму разделять классы по назначению. Но еще есть над чем поработать (например, вынести отрисовку стека в отдельный класс) Для хранения перемещаемых объектов использовал простой расширяемый пул. Для взаимодействия UI и складов использовал синглтон в виде обработчика событий. Префабы использовал только для единицы продукции.

В качестве идеи использовал обработку руды в несколько этапов.

При масштабировании проекта я бы добавил DI (знаком с VContainer). В него бы добавил сервисы обработки событий, сохранения, базовые настройки, рекламу, аналитику. Также нужен стартовый экран с выбором уровня и настройками. И загрузчик уровней. Ну и желательно звуки.

Использовал ассеты из UnityStore:
-	DOTWeen
-	Набор джойстиков (Joystick Pack)

NavMesh из GitHub

Стандартные ассеты:
-	Cinemachine
-	ShederGraph

П.С. Именование переменных и классов использовал на свое усмотрение (не знаю как у Вас принято в компании)))
