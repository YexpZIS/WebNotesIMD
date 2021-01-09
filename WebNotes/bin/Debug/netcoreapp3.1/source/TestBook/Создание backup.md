Создание backup
====
Нужно очистить систему от мусора перед созданием резервной копии

1. Перезагрузиться в live образ
----
т.к. если все делать из системы будет много лишних файлов<br> 
и ```/var/lib/dhcpcd/proc/kcore = 2 TB```

2. Примонтировать диски
----
	mkdir /mnt/{sdX,sdY} # без пробелов

	mount /dev/sda2 /mnt/sdX # система
	mount /dev/sdb4 /mnt/sdY # диск для сохранения backup

3. Создать архив со сжатием
----

	cd /mnt/sdX 
	tar -cvzpf /mnt/sdY/backup.tar.bz2  *
	

1. Перейти в папку с системой?

	**Yes**
		1. bin
		2. etc
		3. usr
		4. var
		5. root
	**No**
		1. mnt
			1. sdX
				1. bin
				2. etc
				3. usr
				4. var
				5. root

2. Если файл резервной копии сохраняем на тот же диск, то надо исключить сам архив из архивации

```sdU``` = ```/mnt/sdX/mnt/backup.tar.bz2```

	tar -cvzpf sdU --exclude=sdU *	

