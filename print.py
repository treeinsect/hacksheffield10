import requests
import os

url = "http://192.168.43.11:5000/image.jpg"

resp = requests.get(url)
resp.raise_for_status()

with open("image.jpg", "wb") as f:
    f.write(resp.content)

os.system("lp image.jpg")

