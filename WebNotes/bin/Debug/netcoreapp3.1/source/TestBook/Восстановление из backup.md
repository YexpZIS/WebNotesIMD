Восстановление из backup
====

1. Создание разделов на диске
----

1. Просматриваем доступные диски

		fdisk -l 

2. Выбираем диск для разворачивания системы

		fdisk /dev/sdX

3. Создаем таблицу разделов GPT (m for help)
	
		g 

4. Создаем раздел на 100M для загрузчика 

		n 
		+100M

5. Помечаем загрузочный раздел (L fot help)

		t 
		4 # Bios boot

6. Создаем 2-ой раздел под систему

		n

7. Записываем все изменения на диск

		w 

8. Создаем файловую систему для загрузчика

		mkfs.vfat -F 32 /dev/sdX1

9. Создаем файловую систему для linux

		mkfs.ext4 /dev/sdX2


2. Копирование файлов системы
----

1. Монтируем диски

		mkdir /mnt/{sdX,sdY} # без пробелов

		mount /dev/sda2 /mnt/sdX # система
		mount /dev/sdb4 /mnt/sdY # диск с backup

2. Распаковываем систему

````backup```` - /mnt/sdY/backup.tar.bz2<br>
````disk```` - /mnt/sdX

	    tar --same-owner -xvpf backup -C disk

````--same-owner```` - с сохранением прав<br>
````-C disk```` - куда распаковать архив

3. Восстановление загрузчика
----

````system```` - /mnt/sdX - система из архива

1. Генерируем fstab

        genfstab -U system >> system/etc/fstab

2. Входим в систему

        arch-chroot system

3. Устанавливаем GRUB2

        grub-install system # не надо указывать номер раздела

4. Генерируем конфикурацию загрузчика
		
	    grub-mkconfig -o /boot/grub/grub.cf


