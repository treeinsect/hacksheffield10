from flask import Flask, make_response, request, redirect, render_template, send_from_directory,send_file
from random import choices as rand_choices
from json import load as json_load

app = Flask(__name__)


#endpoint should be / image name
@app.get("/<image_name>.jpg")
def passImageToPrinter(image_name):
    return send_file("{image}.jpg".format(image=image_name),"image/jpeg")#.format(name=image_name))
    

app.run(host="0.0.0.0")