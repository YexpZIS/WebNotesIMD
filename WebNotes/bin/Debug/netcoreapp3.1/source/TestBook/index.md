GPG. Шифрование в линуксе.
====

gpg, pass
(все в этом блоке считаетс тектом для поиска т.к. не относится к основной статье
(проиндексировать базу данных))

GPG
----
GNU Privacy Guard (GnuPG, GPG) — свободная программа для шифрования информации и создания электронных цифровых подписей. 


Разработана как альтернатива PGP и выпущена под свободной лицензией GNU General Public License. GnuPG полностью совместима со стандартом IETF OpenPGP. Текущие версии GnuPG могут взаимодействовать с PGP и другими OpenPGP-совместимыми системами.

Бэкапим секретный ключ
----
Some text

    gpg -a --export johenews > public_key.gpg
    rm -rf

Очень важно хранить секретный ключ в супернадежном месте.

`revocation certificate` тоже важно хранить где-либо куда никто кроме вас не имеет доступ. 

Список
----
Some text

    Список оптределяется по пробелам или табам перед '---'

    1. Монтируем диски
    ----

    Some text

        mkdir /mnt/{sdX,sdY} # без пробелов

        mount /dev/sda2 /mnt/sdX # система
        mount /dev/sdb4 /mnt/sdY # диск с backup

    2. Солим ключ
    ----
        gpg -a --export-secret-key johenews > secret_key

Карусель изображений
----
![img](source/1.png) // нужно сгруппировать
![img](source/2.png)
![img](source/3.jpg)

![img](source/1.png) // отдельное изображение

![au](source/123.mp3)

![v](source/v.mp4)

Downloads
----
![d](path/to/file)