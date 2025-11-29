from flask import Flask, make_response, request, redirect, render_template, send_from_directory
from random import choices as rand_choices
from string import ascii_letters
from sqlite3 import connect
from json import load as json_load
from os import path

app = Flask(__name__)

app.post("/")
def passImageToPrinter():
    pass

app.run(host="0.0.0.0")