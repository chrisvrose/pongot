shader_type canvas_item;
uniform vec3 col;
void vertex() {
	// Called for every vertex the material is visible on.
}

void fragment() {
	// Called for every pixel the material is visible on.
	//vec4 tex = texture(TEXTURE,UV) * vec4(1,1,1,1);
	vec4 color = texture(TEXTURE,UV).a * vec4(col,1);
	COLOR = color;
	//COLOR =  vec4(1,1,1,0.5);
}

//void light() {
//	// Called for every pixel for every light affecting the material.
//	// Uncomment to replace the default light processing function with this one.
//}
