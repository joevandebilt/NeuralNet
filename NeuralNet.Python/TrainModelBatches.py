import numpy
import sys
import nltk
import math

from keras.models import Sequential
from keras.layers import Dense, Dropout, LSTM
from keras.utils import np_utils
from keras.callbacks import ModelCheckpoint

from GenerateData import create_model,generate_data_array, generate_prediction_model, get_model_filename, read_all_scripts, hard_coded_chars

import os

#Main
print('Hello World')

reset_model = 1

file = read_all_scripts()
script_length = len(file)

batches = 90   ##for the number of episodes
batchLength = math.floor(script_length / batches)

print(batchLength)
print('Training model for script')

chars = hard_coded_chars()
bestLoss = math.inf

for i in range(0, script_length - batchLength, batchLength):
    snippet = file[i:i+batchLength]

    x_data, y_data = generate_data_array(snippet, chars)
    X, y = generate_prediction_model(x_data, y_data, chars)

    model = create_model(X, y, reset_model)
    reset_model = 0

    checkpoint = ModelCheckpoint(get_model_filename(), monitor='loss', verbose=1, save_best_only=True, mode='min')
    checkpoint.best = bestLoss
    desired_callbacks = [checkpoint]

    history = model.fit(X, y, epochs=7, batch_size=256, callbacks=desired_callbacks)
    bestLoss = checkpoint.best