import os
import argparse
from check_alphabet import check_alphabet
from CSVgenerator import generate_csv


def main(args):
    transcript_path = args.transcriptPath
    alphabet_path = args.alphabetPath

    check_alphabet(transcript_path, alphabet_path)

    generate_csv(args.csvPath, args.wavPath, transcript_path)


if __name__ == '__main__':
    parser = argparse.ArgumentParser()
    parser.add_argument('--alphabetPath', type=str)
    parser.add_argument('--transcriptPath', type=str)
    parser.add_argument('--wavPath', type=str)
    parser.add_argument('--csvPath', type=str)

    args = parser.parse_args()

    main(args)
