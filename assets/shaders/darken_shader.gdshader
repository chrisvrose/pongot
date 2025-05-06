shader_type canvas_item;

uniform float darkenFactor: hint_range(1.0,255) = 5;

/**
 * Darken a color by factor
 */
void fragment() {
	vec4 color = texture(TEXTURE,UV);
	color.rgb/=darkenFactor;
	COLOR = color;
}