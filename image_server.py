from flask import Flask, send_file

app = Flask(__name__)

#endpoint should be / image name
@app.get("/<image_name>.jpg")
def passImageToPrinter(image_name: str):
    return send_file("{image}.jpg".format(image=image_name),"image/jpeg")

app.run(host="0.0.0.0",port="8080")