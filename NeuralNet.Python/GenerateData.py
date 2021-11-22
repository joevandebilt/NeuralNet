import numpy
import os

from nltk.tokenize import RegexpTokenizer
from nltk.corpus import stopwords

from keras.utils import np_utils
from keras.models import Sequential
from keras.layers import Dense, Dropout, LSTM, CuDNNLSTM

#Classes
def get_sequence_length():
    return 200

def get_model_filename():
    #return "./NeuralNet.Python/SavedModels/tng_s1_model_weights_saved.hdf5"
    return "./NeuralNet.Python/SavedModels/tng_s1234567_model_weights_saved.hdf5"

def tokenize_words(input):
        input = input

        tokenizer = RegexpTokenizer(r"[\w,.:&']+")
        tokens = tokenizer.tokenize(input)

        filtered = filter(lambda token: token not in stopwords.words('english'), tokens)
        return " ".join(filtered)

def generate_chars(file):
    processed_inputs = tokenize_words(file)

    chars = sorted(list(set(processed_inputs)))

    vocab_len = len(chars)

    print ("Total number of characters:", len(processed_inputs))
    print ("Total vocab:", vocab_len)

    return chars

def hard_coded_chars():
    chars = []
    for i in range(32, 123, 1):
        chars.append(chr(i))
    
    return chars
    #return [' ', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z']

def generate_data_array(file, chars):

    processed_inputs = tokenize_words(file)
    input_len = len(processed_inputs)

    x_data = []
    y_data = []

    char_to_num = dict((c, i) for i, c in enumerate(chars))

    # loop through inputs, start at the beginning and go until we hit
    # the final character we can create a sequence out of
    for i in range(0, input_len - get_sequence_length(), 1):
        # Define input and output sequences
        # Input is the current character plus desired sequence length
        in_seq = processed_inputs[i:i + get_sequence_length()]

        # Out sequence is the initial character plus total sequence length
        out_seq = processed_inputs[i + get_sequence_length()]

        # We now convert list of characters to integers based on
        # previously and add the values to our lists
        x_data.append([char_to_num[char] for char in in_seq])
        y_data.append(char_to_num[out_seq])

    return x_data, y_data

def generate_prediction_model(x_data, y_data, chars):
    n_patterns = len(x_data)
    print ("Total Patterns:", n_patterns)

    X = numpy.reshape(x_data, (n_patterns, get_sequence_length(), 1))
    X = X/float(len(chars))

    y = np_utils.to_categorical(y_data, len(chars))

    return X, y

def create_model(X, y, new_model):

    model = Sequential()
    model.add(CuDNNLSTM(256, input_shape=(X.shape[1], X.shape[2]), return_sequences=True))
    model.add(Dropout(0.2))
    model.add(CuDNNLSTM(256, return_sequences=True))
    model.add(Dropout(0.2))
    model.add(CuDNNLSTM(128))
    model.add(Dropout(0.2))
    model.add(Dense(y.shape[1], activation='softmax'))

    if (new_model == 0):
        model.load_weights(get_model_filename())
    
    model.compile(loss='categorical_crossentropy', optimizer='adam')

    return model

def read_all_scripts():
    file_content = ""
    for root, dirs, files in os.walk('./Data/Feed'):
        for file in files:
            if file.endswith('txt'):
                script_path = os.path.join(root, file)

                print('Reading script' + script_path)
                file_content += open(script_path).read()
    print('File Length is '+str(len(file_content)))

    return file_content