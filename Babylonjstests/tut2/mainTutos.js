window.onload = function() {
	var canvas = document.getElementById("canvas");

	if (!BABYLON.Engine.isSupported()) {
		window.alert("Browser not supported");
	}
	else {
		var engine = new BABYLON.Engine(canvas, true);
		scene = createSceneTuto(engine);

		scene.activeCamera.attachControl(canvas);

		engine.runRenderLoop(function() {
			scene.render();
		});

		window.addEventListner("resize", function() {
			engine.resize();
		});
	}
}