import numpy
import sys
import nltk
import math

from keras.models import Sequential
from keras.layers import Dense, Dropout, LSTM
from keras.utils import np_utils
from keras.callbacks import ModelCheckpoint

from GenerateData import tokenize_words, create_model,generate_data_array, generate_prediction_model, get_model_filename, read_all_scripts, hard_coded_chars

import os

#Main
print('Hello World')
bestFit = math.inf

file = read_all_scripts()

print('Training model for script')

chars = hard_coded_chars()
x_data, y_data = generate_data_array(file, chars)
X, y = generate_prediction_model(x_data, y_data, chars)

model = create_model(X, y, 0)

checkpoint = ModelCheckpoint(get_model_filename(), monitor='loss', verbose=1, save_best_only=True, mode='min')
checkpoint.best = bestFit
desired_callbacks = [checkpoint]

#history = model.fit(X, y, epochs=150, batch_size=512, callbacks=desired_callbacks)
bestFit = checkpoint.best

num_to_char = dict((i, c) for i, c in enumerate(chars))

start = numpy.random.randint(0, len(x_data) - 1)
text_output = x_data[start]
pattern = text_output.copy()

for i in range(500):

    x = numpy.reshape(pattern, (1, len(pattern), 1))
    x = x / float(len(chars))

    prediction = model.predict(x, verbose=0)

    index = numpy.argmax(prediction)

    #result = num_to_char[index]
    #sys.stdout.write(result)

    text_output.append(index)
    pattern.append(index)
    pattern = pattern[1:len(pattern)]

print("\r\n\r\n\"", ''.join([num_to_char[value] for value in text_output]), "\"\r\n")