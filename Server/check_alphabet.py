def check_alphabet(transcript_path, alphabet_path):
    """
     Параметры
           ----------
           transcript_path : str
               Путь до текстового транскрипта аудиофайлов
           transcript_path : str
               Путь до алфавита
           ----------
    Проверяем файлы с текстовым траснкриптом на наличие лишних символо, не входящих в алфавит
    """

    with open(alphabet_path, 'r') as alphabet_file:
        alphabet = alphabet_file.read().splitlines()

    with open(transcript_path, 'r') as f:
        cleaned = ''

        for line in f:
            for c in line:

                if c.lower() in alphabet or c == ' ' or c == '\n':
                    cleaned += c.lower()
                if c == '’':
                    cleaned += '\''

        open(transcript_path, 'w').write(cleaned)
