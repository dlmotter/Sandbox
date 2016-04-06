# Daniel Motter
# Steganography sandbox
# https://en.wikipedia.org/wiki/Steganography#Digital_messages

from PIL import Image

def encrypt_image(img1, img2, img3):
    inimg_sup = Image.open(img1).convert('RGB')
    inimg_sub = Image.open(img2).convert('RGB')    
    if inimg_sup.size[0] >= inimg_sub.size[0] and inimg_sup.size[1] >= inimg_sub.size[1]:
        outimg = Image.new('RGB', inimg_sup.size, 'white')
        inpixels_sup = inimg_sup.load()
        inpixels_sub = inimg_sub.load()
        outpixels = outimg.load()
        for i in range(inimg_sup.size[0]):
            for j in range(inimg_sup.size[1]):
                if i < inimg_sub.size[0] and j < inimg_sub.size[1]:
                    outpixels[i, j] = encrypt_pixel(inpixels_sup[i, j][0], inpixels_sup[i, j][1], inpixels_sup[i, j][2], inpixels_sub[i, j][0], inpixels_sub[i, j][1], inpixels_sub[i, j][2])
                else:
                    outpixels[i, j] = encrypt_pixel(inpixels_sup[i, j][0], inpixels_sup[i, j][1], inpixels_sup[i, j][2], 255, 255, 255)
        outimg.save(img3)
        return True
    else:
        print('The image to hide must be smaller in both dimensions than the container image.')
        return False

def decrypt_image(img1, img2):
    inimg = Image.open(img1).convert('RGB')
    outimg = Image.new('RGB', inimg.size, 'black')    
    inpixels = inimg.load()
    outpixels = outimg.load()
    for i in range(inimg.size[0]):
        for j in range(inimg.size[1]):
            outpixels[i, j] = decrypt_pixel(inpixels[i, j][0], inpixels[i, j][1], inpixels[i, j][2])
    outimg.save(img2)
    return True

def encrypt_pixel(r_super, g_super, b_super, r_sub, g_sub, b_sub):
    r_sup_bin = format(r_super, '#010b')[2:8]
    g_sup_bin = format(g_super, '#010b')[2:8]
    b_sup_bin = format(b_super, '#010b')[2:8]
    r_sub_bin = format(r_sub // 64, '#010b')[-2:]
    g_sub_bin = format(g_sub // 64, '#010b')[-2:]
    b_sub_bin = format(b_sub // 64, '#010b')[-2:]    
    return int(r_sup_bin + r_sub_bin, 2), int(g_sup_bin + g_sub_bin, 2), int(b_sup_bin + b_sub_bin, 2)

def decrypt_pixel(r, g, b):
    return int(format(r, '#010b')[-2:] * 4, 2), int(format(g, '#010b')[-2:] * 4, 2), int(format(b, '#010b')[-2:] * 4, 2)

if __name__ == '__main__':
    print('Steganography Demo')
    print('=======================')

    super_img = input('Container image: ')
    sub_img = input('Secret image: ')
    encrypted_image = super_img.split('.')[-2] + '.encrypted.' + super_img.split('.')[-1]

    print('Encrypting...')
    if encrypt_image(super_img, sub_img, encrypted_image) and input('Decrypt result? (Y/N): ').upper() == 'Y':
        print('Decrypting...')
        decrypted_image = super_img.split('.')[-2] + '.decrypted.' + super_img.split('.')[-1]
        decrypt_image(encrypted_image, decrypted_image)
