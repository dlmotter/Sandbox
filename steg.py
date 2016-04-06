# Daniel Motter
# Steganography sandbox
# https://en.wikipedia.org/wiki/Steganography#Digital_messages

# Essentially, take two 256 bit images, make one 252 bit (the "super" or "hider" image), and one 4 bit (the "sub" or "hidden" image)
# Then, hide the 4 bit one in the newly opened up space in the 252 bit one.
# Since 252 and 256 are very close, humans cannot tell that something is fishy.

from PIL import Image

#img1 is the super image
#img2 is the sub image
#img3 is the encrypted, resultant image
def encrypt_image(img1, img2, img3):
    inimg_sup = Image.open(img1).convert('RGB')
    inimg_sub = Image.open(img2).convert('RGB')
    #Ensure the container image can contain the hidden image
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

#img1 is the encrypted image
#img2 is the decrypted, resultant image
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
    # Keep 6 most significant bits of each color of the super pixel (format as 8 binary digits preceeded by 0 if necessary, then take first 6 digits)
    #  Ex. 10011010 becomes 100110__
    r_sup_bin = format(r_super, '08b')[:6]
    g_sup_bin = format(g_super, '08b')[:6]
    b_sup_bin = format(b_super, '08b')[:6]
    # For each color value of the sub pixel, if it is
    #    0 -  63, append 00
    #   64 - 127, append 01
    #  128 - 191, append 10
    #  192 - 255, append 11
    r_sub_bin = format(r_sub // 64, '08b')[-2:]
    g_sub_bin = format(g_sub // 64, '08b')[-2:]
    b_sub_bin = format(b_sub // 64, '08b')[-2:]
    # Return the encoded values back as decimals
    return int(r_sup_bin + r_sub_bin, 2), int(g_sup_bin + g_sub_bin, 2), int(b_sup_bin + b_sub_bin, 2)

def decrypt_pixel(r, g, b):
    # Take the least two significant bits from each color. If it is 
    #  00, make it 00000000, which is 0
    #  01, make it 01010101, which is 85
    #  10, make it 10101010, which is 170
    #  11, make it 11111111, which is 255
    return int(format(r, '08b')[-2:] * 4, 2), int(format(g, '08b')[-2:] * 4, 2), int(format(b, '08b')[-2:] * 4, 2)

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
