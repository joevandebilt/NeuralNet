
import numpy
import math
import os
from GenerateData import hard_coded_chars, generate_data_array, generate_prediction_model, create_model, read_all_scripts

#How many characters to generate

file_content = ""    
for root, dirs, files in os.walk('./Data/TNG Scripts'):
    for file in files:
        if file.endswith('txt'):
            script_path = os.path.join(root, file)

            print('Reading script' + script_path)
            file_content += open(script_path).read()

chars = hard_coded_chars()

script_length = len(file_content) 
batches = 178   ##for the number of episodes
batchLength = math.floor(script_length / batches)
start = numpy.random.randint(0, script_length - batchLength-1)

snippet = file_content[start:start+batchLength]

x_data, y_data = generate_data_array(snippet, chars)

X, y = generate_prediction_model(x_data, y_data, chars)

model = create_model(X, y)

num_to_char = dict((i, c) for i, c in enumerate(chars))

start = numpy.random.randint(0, len(x_data) - 1)
text_output = x_data[start]
pattern = text_output.copy()

for i in range(1000):

    x = numpy.reshape(pattern, (1, len(pattern), 1))
    x = x / float(len(chars))

    prediction = model.predict(x, verbose=0)

    index = numpy.argmax(prediction)

    #result = num_to_char[index]
    #sys.stdout.write(result)

    text_output.append(index)
    pattern.append(index)
    pattern = pattern[1:len(pattern)]

print("\r\n\t\"", ''.join([num_to_char[value] for value in text_output]), "\"")