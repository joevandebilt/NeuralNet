import tensorflow as tf
from tensorflow.keras.layers.experimental import preprocessing

import numpy as np
import os
import time

from GenerateData import read_all_scripts

text = read_all_scripts()
print(f'Length of text: {len(text)} characters')

print(text[:250])

vocab = sorted(set(text))
print(f'{len(vocab)} unique characters')