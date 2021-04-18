# crispy-disco
###Part 1:
###Create "Character" class and add fields:
- ~~unique id~~ (*);  **need to be tested**
- ~~name~~ (*); 
- ~~state (normal, weakened, sick, poisoned, paralyzed, dead);~~
- ~~ability to speak at the moment~~
- ~~ability to move at the moment~~;
- ~~race (human, gnome, elf, orc, goblin)~~ (*);
- ~~gender~~ (*);
- ~~age~~;
- ~~current HP~~; **probably make uint**
- ~~maximum HP~~;
- ~~XP~~.
### * is a flag for fields that can't be changed after constructor
###Implement:
- ~~constructor that sets fields that can't be changed~~ and unique id;
- ~~properties~~;
- ~~IComparable(comparison by XP)~~;
- ~~state changing~~.
- ~~character info output with overrided ToString() method~~.
  
 ### Создать класс-потомок «персонаж, владеющий магией». Дополнительно включить в описание этого класса следующие поля:
- текущее значение магической энергии (маны) (неотрицательная величина);
- максимальное значение маны.
  Мана расходуется на произнесение заклинаний. Если текущее значение маны
  меньше того количества, которое требуется для произнесения какого-либо
  заклинания, заклинание не может быть произнесено, а количество маны остается
  неизменным.
  Некоторые заклинания обладают силой, причем сила заклинания задается
  волшебником в момент его произнесения. Расход маны в этом случае
  пропорционален силе заклинания. Сила заклинания ограничивается текущим
  значением маны.
  Реализовать заклинание «добавление здоровья». Суть этого заклинания – увеличить
  текущее значение здоровья какого-либо персонажа (в том числе и себя) до
  максимального или до предела, задаваемого текущим значением маны. На единицу
  добавленного здоровья расходуется две единицы маны.

Часть 2
Создать интерфейс «волшебство», включающий перегруженные методы
«выполнить волшебное воздействие». В качестве параметра указать персонажа
ролевой игры, на которого может быть направлено воздействие, а также силу
воздействия. Оба параметра могут отсутствовать.
Создать абстрактный класс «заклинание», реализующий указанный интерфейс.
Включить в описание класса следующие поля:
- минимальное значение маны, требуемое для выполнения заклинания (может
  быть равно 0);
- наличие вербальной компоненты (заклинание нужно произносить);
- наличие моторной компоненты (необходимо выполнять какие-то движения);
  Реализовать классы заклинаний:
1) «Добавить здоровье». Суть этого заклинания – увеличить текущее значение
   здоровья какого-либо персонажа на заданную величину или до предела,
   задаваемого текущим значением маны. На единицу добавленного здоровья
   расходуется две единицы маны.
2) «Вылечить». Суть этого заклинания – перевести какого-либо персонажа из
   состояния «болен» в состояние «здоров или ослаблен». Текущая величина
   здоровья не изменяется. Заклинание требует 20 единиц маны.
3) «Противоядие». Суть этого заклинания – перевести какого-либо персонажа
   из состояния «отравлен» в состояние «здоров или ослаблен». Текущая
   величина здоровья не изменяется. Заклинание требует 30 единиц маны.
4) «Оживить». Суть этого заклинания – перевести какого-либо персонажа из
   состояния «мертв» в состояние «здоров или ослаблен». Текущая величина
   здоровья становится равной 1. Заклинание требует 150 единиц маны.
5) «Броня». Персонаж, на которого обращено заклинание, становится
   неуязвимым в течение некоторого промежутка времени, определяемого
   силой заклинания. Заклинание требует 50 единиц маны на единицу времени.
6) «Отомри!» Суть этого заклинания – перевести какого-либо персонажа из
   состояния «парализован» в состояние «здоров или ослаблен». Текущая
   величина здоровья становится равной 1. Заклинание требует 85 единиц маны.
   Создать абстрактный класс «артефакт», реализующий указанный интерфейс.
   Включить в описание класса следующие поля:
- мощность артефакта (величина, аналогичная количеству маны у персонажа,
  владеющего магией) – может быть равен 0;
- признак возобновляемости артефакта.
  Реализовать классы артефактов:
1) ~~Бутылка с живой водой – увеличивает здоровье персонажа. Здоровье
   персонажа не может превысить максимальную величину, но артефакт
   используется полностью! Могут быть малые, средние и большие бутылки,
   увеличивающие здоровье соответственно на 10, 25 и 50 единиц. Не
   возобновляемый.~~
2) ~~Бутылка с мертвой водой – увеличивает ману персонажа, владеющего
   магией. Мана не может превысить максимальную величину, но артефакт
   используется полностью! Могут быть малые, средние и большие бутылки
   увеличивающие ману соответственно на 10, 25 и 50 единиц. Не
   возобновляемый.~~
3) ~~Посох «Молния». Уменьшает количество здоровья персонажа, против
   которого был применен этот артефакт, на величину, заданную мощностью
   (мощность задаётся персонажем при использовании артефакта). Мощность
посоха уменьшается на эту величину. Возобновляемый, но непригоден для
использования, если его мощность равна нулю.~~
4) ~~Декокт из лягушачьих лапок. Переводит какого-либо персонажа из состояния
   «отравлен» в состояние «здоров или ослаблен». Текущая величина здоровья
   не изменяется. Не возобновляемый.~~
5) ~~Ядовитая слюна (накладка на зубы, через которую надо плевать). Переводит
   какого-либо персонажа из состояния «здоров или ослаблен» в состояние
   «отравлен». Текущая величина здоровья уменьшается на величину,
   задаваемую мощностью артефакта. При применении этого артефакта
   персонаж, против которого он был применен, может умереть!
   Возобновляемый.~~
6) ~~Глаз василиска. Переводит любого не мёртвого персонажа в состояние
   «парализован». Не возобновляемый.
   Реализовать лечение, которое было описано в части 1, через произнесение
   соответствующего заклинания и использование артефакта.~~
   Часть 3
   Дополнить созданные в частях 1 и 2 классы следующими возможностями:
1) ~~У каждого персонажа игры есть мешок (inventory), куда можно помещать
   различные артефакты (количество артефактов одного вида неограниченно) и
   использовать их. Если артефакт не является возобновляемым, он исчезает из
   мешка. Можно использовать только те артефакты, которые имеются в мешке.~~
2) Персонаж, владеющий магией, может изучить различные заклинания. После
   изучения заклинания могут быть реализованы. Можно реализовывать только
   изученные заклинания.
   
Реализовать методы:
- ~~«Подобрать артефакт и пополнить мешок»~~
- ~~«Выбросить артефакт из мешка»~~
- ~~«Передать артефакт другому персонажу»~~
- ~~«Использовать артефакт»~~
- «Выучить заклинание»
- «Забыть заклинание»
- «Произнести заклинание»