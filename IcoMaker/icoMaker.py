import tkinter as tk
from tkinter import filedialog
from tkinter import messagebox
from PIL import Image

def convert_image():
    input_file = filedialog.askopenfilename(filetypes=[("Image files", "*.png;*.jpg;*.jpeg")])
    if not input_file:
        return

    output_file = filedialog.asksaveasfilename(defaultextension=".ico", filetypes=[("ICO files", "*.ico")])
    if not output_file:
        return

    try:
        img = Image.open(input_file)
        img = img.convert("RGBA")
        img.save(output_file, format="ICO", sizes=[(16, 16), (32, 32), (48, 48), (64, 64), (128, 128), (256, 256)])
        tk.messagebox.showinfo("Success", f"ICO file created: {output_file}")
    except Exception as e:
        tk.messagebox.showerror("Error", f"Failed to convert image: {e}")

# GUI Setup
root = tk.Tk()
root.title("ICO Converter")
root.geometry("300x100")

convert_button = tk.Button(root, text="Convert to ICO", command=convert_image)
convert_button.pack(expand=True)

root.mainloop()
