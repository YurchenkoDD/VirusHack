import pandas as pd
import os
from natsort import natsorted
import numpy as np

def generate_csv(csv_path, wav_path, transcript_path, train_percent=.60, val_percent=.20, test_percent=.20):
    """
           Параметры
           ----------
           csv_path : str
               Путь для сохранения сгенерированных .csv файлов
           wav_path : str
               Путь до аудиафайлов
           transcript_path : str
               Путь до текстового транскрипта аудиофайлов
            train_percent, val_percent, test_percent: float
                распределение данных для обучения
           """

    wav_files = list(map(lambda file: os.path.join(wav_path, file),
                         [file for file in os.listdir(wav_path) if not file.startswith('.')]))
    wav_files = natsorted(wav_files)

    wav_sizes = [os.stat(f).st_size for f in wav_files]

    with open(transcript_path, 'r') as f:
        transcript_lines = [line[:-1] for line in f]

    train_indices, val_indices, test_indices = get_indices(len(wav_files), train_percent, val_percent, test_percent)

    train_wav_files = []
    train_wav_sizes = []
    train_transcript_lines = []

    val_wav_files = []
    val_wav_sizes = []
    val_transcript_lines = []

    test_wav_files = []
    test_wav_sizes = []
    test_transcript_lines = []

    for i, file in enumerate(wav_files):

        if i in train_indices:
            train_wav_files.append(wav_files[i])
            train_wav_sizes.append(wav_sizes[i])
            train_transcript_lines.append(transcript_lines[i])
        elif i in val_indices:
            val_wav_files.append(wav_files[i])
            val_wav_sizes.append(wav_sizes[i])
            val_transcript_lines.append(transcript_lines[i])
        elif i in test_indices:
            test_wav_files.append(wav_files[i])
            test_wav_sizes.append(wav_sizes[i])
            test_transcript_lines.append(transcript_lines[i])

    train_csv = pd.DataFrame(columns=['wav_filename', 'wav_filesize', 'transcript'])
    train_csv['wav_filename'] = train_wav_files
    train_csv['wav_filesize'] = train_wav_sizes
    train_csv['transcript'] = train_transcript_lines

    val_csv = pd.DataFrame(columns=['wav_filename', 'wav_filesize', 'transcript'])
    val_csv['wav_filename'] = val_wav_files
    val_csv['wav_filesize'] = val_wav_sizes
    val_csv['transcript'] = val_transcript_lines

    test_csv = pd.DataFrame(columns=['wav_filename', 'wav_filesize', 'transcript'])
    test_csv['wav_filename'] = test_wav_files
    test_csv['wav_filesize'] = test_wav_sizes
    test_csv['transcript'] = test_transcript_lines

    train_csv.to_csv(os.path.join(csv_path, 'train.csv'), encoding='utf-8', index=False)
    val_csv.to_csv(os.path.join(csv_path, 'val.csv'), encoding='utf-8', index=False)
    test_csv.to_csv(os.path.join(csv_path, 'test.csv'), encoding='utf-8', index=False)


def get_indices(n, train_percent, val_percent, test_percent):
    assert train_percent <= 1 and val_percent <= 1 and test_percent <= 1
    assert train_percent + val_percent + test_percent == 1
    assert train_percent >= 0 and val_percent >= 0 and test_percent >= 0

    non_test_indices = np.random.choice(n, int(n * (1 - test_percent)), replace=False)

    test_indices = np.setdiff1d(np.arange(n), non_test_indices, assume_unique=False)

    len_non_train_indices = len(non_test_indices)

    val_indices = np.random.choice(non_test_indices,
                                   int((len_non_train_indices - 1) * val_percent / (1 - test_percent)),
                                   replace=False)

    train_indices = np.setdiff1d(non_test_indices, val_indices)

    return train_indices, val_indices, test_indices
