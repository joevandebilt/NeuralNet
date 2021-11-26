from tensorflow.keras.preprocessing import text
from textgenrnn import textgenrnn


#textgen = textgenrnn()
#textgen.train_from_largetext_file("./Data/Combined.txt", new_model=True)
#textgen.save("./NeuralNet.Python/SavedModels/textgenrnn_model.hdf5")
#textgen.generate()

weights_path = "./NeuralNet.Python/SavedModels/textgenrnn_model.hdf5"
vocab_path = "./NeuralNet.Python/SavedModels/textgenrnn_vocab.json"

textgen = textgenrnn(weights_path=weights_path, vocab_path=vocab_path)
#textgen.train_from_largetext_file("./Data/Combined.txt", new_model=False)
textgen.generate(3, temperature=0.3, max_gen_length=500)