VM OS = Ubuntu 20.04.2 LTSs
VM Version = Oracle VM Virtual Box (Lastest Version)
[Installation]
Install apache, mysql, rabbitmq, git

Clone github.com/jefuhr/tentative

Change IP in dbRabbitMQ.ini
Import ddump.db into mysql
Run DIR/database/listener/endpoint.php

[For-Database-Replication]
Follow all installation steps up to importing ddump.db
Set up mysql circular replication for all systems
Run DIR/database/listener/endpoint.php

[Post-Installation]
Change IP in dbRabbitMQ.ini
Run DIR/database/listener/endpoint.php