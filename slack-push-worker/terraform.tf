terraform {
  required_providers {
    docker = {
      source  = "kreuzwerker/docker"
      version = "3.0.2"
    }
  }
}

provider "docker" {
  host = "unix:///var/run/docker.sock"
}

resource "docker_image" "redis" {
  name = "redis:alpine"
}

# Create a container
resource "docker_container" "foo" {
  image = docker_image.redis.image_id
  name  = "foo"
}
