def main():
    message = input("Enter your message: ")
    print("Ciphertext: " + encrypt(message))
    print("Decrypted ciphertext: " + encrypt(encrypt(message)))

def encrypt(message):
    ciphertext = ''
    for ch in message:
        if ch.isalpha():
            if ch.isupper():
                ciphertext += chr((91-ord(ch)) + 64)
            else:
                ciphertext += chr((123-ord(ch)) + 96)
        else:
            ciphertext += ch
    return ciphertext

if __name__ == '__main__':
    main()
