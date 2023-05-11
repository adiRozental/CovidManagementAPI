import tkinter as tk
from tkinter import ttk
import math

TYPE_OF = ""


def draw_rectangular_tower():
    """The function transform the window to a rectangle calculations window"""
    make_visible()
    global TYPE_OF
    TYPE_OF = "rectangle"


def draw_triangular_tower():
    """The function transform the window to a triangle calculations window"""
    make_visible()
    perimeter_button.grid(row=1, column=1, padx=5, pady=10)
    area_button.grid(row=1, column=3, padx=5, pady=10)
    calculate_button.grid_forget()
    global TYPE_OF
    TYPE_OF = "triangle"


def validation():
    """The function gets the values of the height and width textbox and checks if they are valid"""
    try:
        height = int(height_entry.get())
        width = int(width_entry.get())
    except ValueError:
        # Handle the case where the user input is not a valid integer
        tower_label.config(text="Invalid input: height and width must be integers")
        return None, None
    if height * width <= 0:
        # Handle the case where the user input for one of the values is smaller (or =) than 0
        tower_label.config(text="Invalid input: height and width must be bigger than 0")
        return None, None
    return height, width


def perimeter():
    """The function calculates the perimeter of an isosceles triangle"""
    height, width = validation()
    if height is None:
        return
    side = math.sqrt((height ** 2) + ((width / 2) ** 2))  # calculating the sides using pythagoras formula
    tower_text = "perimeter: " + str(side * 2 + width)
    tower_label.config(text=tower_text)
    back_button.grid()


def triangle_print(height, width):
    """The function creates the printing of the triangle by its height and width"""
    tower_text = ""
    if width % 2 == 0 or height * 2 < width:
        # Handle the case where the user input is not valid for a triangle
        tower_text = "ERROR! try another triangle. \nNote: width must be an odd number."
    else:
        num_groups = int(width / 2) - 1
        if num_groups == 0 and height > 2:
            # Handle the case where the user input is not valid for our printing method
            tower_text = "ERROR! cant print this triangle, please try another."
        else:
            my_array = [0] * int(num_groups)  # resetting the array
            for i in range(height - 2):  # calculating hoe many levels of each layer (top first)
                my_array[i % num_groups] += 1
            my_array = [1] + my_array + [1]  # adding the top and base layers
            count = 1  # the count of levels of each area, starting by 1 and adding two more for every layer
            for i in range(len(my_array)):  # adding each level to the print string
                tower_text += (" " * (height - i - 1) + "*" * count + "\n") * my_array[i]
                count += 2
    return tower_text


def calc_program():
    """the function activates and calculates according to user choice"""
    height, width = validation()
    if height is None:
        return

    if TYPE_OF == "triangle":
        if height == 1 or width == 1:
            tower_label.config(text="Invalid input: triangle height and width must be bigger than 1")
            return
        tower_text = triangle_print(height, width)
        tower_label.config(text=tower_text)
    elif TYPE_OF == "rectangle":
        if abs(height - width) > 5:
            tower_text = "area: " + str(height * width)
        else:
            tower_text = "perimeter: " + str(height * 2 + width * 2)
        tower_label.config(text=tower_text)
    back_button.grid()  # activates the back to menu button


def back_to_menu():
    """the function costumes the window to the menu window"""
    back_button.grid_forget()
    calculate_button.grid_forget()
    tower_label.config(text="")
    height_entry.delete(0, "end")
    width_entry.delete(0, "end")
    height_label.grid_forget()
    height_entry.grid_forget()
    width_entry.grid_forget()
    width_label.grid_forget()
    perimeter_button.grid_forget()
    area_button.grid_forget()
    subtitle_label.grid(row=0, column=0, padx=5, pady=10)
    triangular_button.grid(row=1, column=2, padx=5, pady=10)
    rectangular_button.grid(row=1, column=1, padx=5, pady=10)
    exit_button.grid()
    global TYPE_OF
    TYPE_OF = ""


def make_visible():
    """the function costumes the window to the calculation window"""
    triangular_button.grid_forget()
    rectangular_button.grid_forget()
    subtitle_label.grid_forget()
    exit_button.grid_forget()
    calculate_button.grid()

    height_label.grid(row=0, column=0, padx=5, pady=10)
    height_entry.grid(row=0, column=1, padx=5, pady=10)
    width_entry.grid(row=0, column=3, padx=5, pady=10)
    width_label.grid(row=0, column=2, padx=5, pady=10)


def exit_program():
    """The function closed the program"""
    window.destroy()


# Creating the window
window = tk.Tk()
window.title("Tower Drawing Program")
window.geometry("700x400")

# creating the styles we will use
style = ttk.Style()
style.configure("Title.TLabel", font=("Helvetica", 18, "bold"), foreground="#353535")
style.configure("Subtitle.TLabel", font=("Helvetica", 12), foreground="#353535")
style.configure("TButton", font=("Helvetica", 12), foreground="#353535", background="#353535")
style.map("TButton", foreground=[("disabled", "#AAAAAA")])
style.configure("TEntry", font=("Helvetica", 12), foreground="#353535")
style.configure("Tower.TLabel", font=("Courier New", 12), foreground="#353535")

# Creating and adding the widgets to the window
title_label = ttk.Label(window, text="Tower Drawing Program", style="Title.TLabel")
title_label.pack(pady=10)

option_frame = ttk.Frame(window, padding=10)
option_frame.pack()

subtitle_label = ttk.Label(option_frame, text="Select a tower type:", style="Subtitle.TLabel")
subtitle_label.grid()

button_frame = ttk.Frame(option_frame)
button_frame.grid()

rectangular_button = ttk.Button(button_frame, text="Rectangular", width=12, style="TButton",
                                command=draw_rectangular_tower)
rectangular_button.grid(row=1, column=1, padx=5, pady=10)

triangular_button = ttk.Button(button_frame, text="Triangular", width=12, style="TButton",
                               command=draw_triangular_tower)
triangular_button.grid(row=1, column=3, padx=5, pady=10)

perimeter_button = ttk.Button(button_frame, text="Perimeter", width=12, style="TButton",
                              command=perimeter)
area_button = ttk.Button(button_frame, text="print", width=12, style="TButton",
                         command=calc_program)
exit_button = ttk.Button(option_frame, text="Exit", width=12, style="TButton", command=exit_program)
exit_button.grid(row=2, column=0, padx=5, pady=10)
calculate_button = ttk.Button(option_frame, text="calculate", width=12, style="TButton", command=calc_program)
back_button = ttk.Button(option_frame, text="Back to menu", width=12, style="TButton", command=back_to_menu)

input_frame = ttk.Frame(window, padding=10)
input_frame.pack()

height_label = ttk.Label(input_frame, text="Height:", style="Subtitle.TLabel")
height_entry = ttk.Entry(input_frame, width=5, style="TEntry")
width_label = ttk.Label(input_frame, text="Width:", style="Subtitle.TLabel")
width_entry = ttk.Entry(input_frame, width=5, style="TEntry")

tower_frame = ttk.Frame(window, padding=10)
tower_frame.pack()

tower_label = ttk.Label(tower_frame, text="", font=("Courier New", 12), justify="left", style="Tower.TLabel")
tower_label.pack()

window.mainloop()  # running the main event loop of the window
