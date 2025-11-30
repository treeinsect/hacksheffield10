from flask import Flask, send_file, request
from print import print_image

app = Flask(__name__)


#endpoint should be / image name
@app.get("/<image_name>.jpg")
def passImageToPrinter(image_name: str):
    return send_file("{image}.jpg".format(image=image_name),"image/jpeg")


@app.route('/post', methods=["POST"])
def getImageName():
    imageName = request.form.get("imageName")
    print_image(image_name=imageName)
    return "OK"

app.run(host="0.0.0.0")