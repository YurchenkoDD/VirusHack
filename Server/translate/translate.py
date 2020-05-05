import argparse
from googletrans import Translator

def rgt(text, langs, times):
    translator = Translator()

    translation = text
    for i in range(times):
        for lang in langs:
            translation = translator.translate(translation, dest=lang).text
        translation = translator.translate(translation, dest='ru').text

    return translation

def main():
    parser = argparse.ArgumentParser(
        description='Перевод текста с помощью Google Translate')
    group = parser.add_mutually_exclusive_group(required=True)
    group.add_argument('-t', '--text', type=str, help='Входной текст:')
    group.add_argument('-f', '--file', type=str, help='Файл с входным текстом:')
    parser.add_argument('-o', '--out', type=str, help='Файла для выходного текста:')
    parser.add_argument('-l', '--langs', type=str, nargs='+', help='.',
                        default=['en', 'de', 'la', 'it', 'es'])
    args = parser.parse_args()

    if args.file:
        with open(args.file) as f:
            text = f.read()
    else:
        text = args.text

    result = rgt(text, args.langs, args.num)
    if args.out:
        with open(args.out, mode='w') as f:
            f.write(result)
    else:
        print()
        print(result)


if __name__ == '__main__':
    main()
