import requests
import os

def print_image(image_name: str):
    url = "http://192.168.43.62:5000/" + image_name

    resp = requests.get(url)
    resp.raise_for_status()

    with open("image.jpg", "wb") as f:
        f.write(resp.content)

    os.system("lp image.jpg")

