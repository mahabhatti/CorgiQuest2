
for layer in "full" "canopy" "noleaves" "trunk" "shadow-leaves" "shadow-noleaves"; do
	out="./$layer/"
	file="baobab-$layer.png"
	mkdir -p $out
	convert $file -crop 96x128+0+0      $out/adansonia_spp.png
	convert $file -crop 96x128+96+0     $out/a_gregorii.png
	convert $file -crop 96x128+192+0    $out/a_digitata.png
	convert $file -crop 96x128+288+0    $out/a_suarazensis.png
	convert $file -crop 96x160+0+128    $out/a_grandideri.png
	convert $file -crop 96x160+96+128   $out/a_za.png
	convert $file -crop 128x128+192+160 $out/a_rubrostipa.png
done