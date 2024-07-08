from flask import Flask, request, jsonify, render_template
import pickle
import numpy as np
import tensorflow as tf
from tensorflow.keras.preprocessing.text import tokenizer_from_json
from sklearn.preprocessing import MultiLabelBinarizer
import json
import spacy
import os

#### We load our things

# Get the path of the directory where the script is located

path = os.path.dirname(os.path.abspath(__file__))

# Load the saved model

model  = tf.keras.models.load_model(path+'/model_best.h5', compile=False)

# Load the tokenizer
with open(path+'/tokenizer.json') as f:
    data = json.load(f)
    tokenizer = tokenizer_from_json(data)

# Load the MultiLabelBinarizer
with open(path+'/mlb.pkl', 'rb') as f:
    mlb = pickle.load(f)

# Load the spaCy model for NLP tasks
nlp = spacy.load('en_core_web_trf')

#### Let's do it
app = Flask(__name__, template_folder='.')

def extract_aspect_terms(description):
    review = nlp(description)
    aspect_terms = ' '.join([chunk.root.text for chunk in review.noun_chunks if chunk.root.pos_ == 'NOUN'])
    return aspect_terms

@app.route('/')
def home():
    return render_template('index.html')

@app.route('/predict', methods=['POST'])
def predict():
    # Get the data from the request
    data = request.json or request.form
    description = data['description']
    
    # Process the input data
    aspect_terms = extract_aspect_terms(description)
    aspect_tokenized = tokenizer.texts_to_matrix([aspect_terms])
    
    # Predict using the loaded model
    predictions = model.predict(aspect_tokenized)
    predicted_categories = (predictions > 0.5).astype(int)
    predicted_labels = mlb.inverse_transform(predicted_categories)
    
    return jsonify({'PredictedLabels': list(predicted_labels[0])})


if __name__ == '__main__':
    app.run(debug=True)
