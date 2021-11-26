from keras.callbacks import ModelCheckpoint
from GenerateData import tokenize_words, create_model,generate_data_array, generate_prediction_model, get_model_filename, read_all_scripts, hard_coded_chars
import math
import os

#Main
print('Hello World')
print('Training model for script')

chars = hard_coded_chars()
improvements = []
for root, dirs, files in os.walk('./Data/TNG Scripts'):
        for file in files:
            if file.endswith('txt'):
                script_path = os.path.join(root, file)

                print('Reading script' + script_path)
                file = open(script_path).read()

                x_data, y_data = generate_data_array(file, chars)
                X, y = generate_prediction_model(x_data, y_data, chars)

                model = create_model(X, y)

                checkpoint = ModelCheckpoint(get_model_filename(), monitor='loss', verbose=1, save_best_only=True, mode='min')
                desired_callbacks = [checkpoint]

                history = model.fit(X, y, epochs=10, batch_size=256, callbacks=desired_callbacks)
                improvements.append( round(checkpoint.best, 4))

print(improvements)