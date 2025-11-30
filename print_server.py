from flask import Flask, request
from print import print_image

app = Flask(__name__)

@app.route('/post', methods=["POST"])
def getImageName():
    imageName = request.get_json["filepath"]
    #imageName = request.form.get("imageName")
    print_image(image_name=imageName)
    return "OK"

app.run(host="0.0.0.0")