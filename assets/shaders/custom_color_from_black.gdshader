shader_type canvas_item;
uniform vec3 col: source_color;

void fragment() {
	vec4 color = texture(TEXTURE,UV).a * vec4(col,1);
	COLOR = color;//*x;
}
