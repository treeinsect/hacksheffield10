import requests
import os

url = "http://example.com/image.jpg"

fileName = 0

resp = requests.get(url)
resp.raise_for_status()

with open("image.jpg", "wb") as f:
    f.write(resp.content)


os.system("lp image.jpg")

