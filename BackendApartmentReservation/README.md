# Setting up local Kibana

* Download and setup Docker:
    * Linux: https://docs.docker.com/install/linux/docker-ce/ubuntu/
    * Windows (you will have to create an account in Docker cloud): https://docs.docker.com/docker-for-windows/install/
* run `docker pull sebp/elk` from your cmd/bash. This will take quite a while.
* run `docker run -p 5601:5601 -p 9200:9200 -p 5044:5044 -it --name elk sebp/elk`. ELK stack container will be instantiated.
* Visit local Kibana at `http://127.0.0.1:5601`
* Go to `Management -> Index Patterns` and enter `logstash-*`
* After this step, you will be able to see and query all the logs in the `Discover` tab