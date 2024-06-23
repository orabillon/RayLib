from pyray import *

init_window(800, 450, "Hello")
set_target_fps(60)

while not window_should_close():
    begin_drawing()
    clear_background(WHITE)
    draw_text("Hello world", 190, 200, 20, VIOLET)
    end_drawing()

close_window()
