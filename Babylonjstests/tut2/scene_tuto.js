function createSceneTuto(engine) {
	var scene = new BABYLON.Scene(engine);

	var light = new BABYLON.PointLight("Omni", new BABYLON.Vector3(0, 100, 100), scene);

	var camera = new BABYLON.ArcRotateCamera("Camera", 0, 0.8, 100, new BABYLON.Vector3.Zero(), scene);

	var box = BABYLON.Mesh.CreateBox("Box", 6.0, scene);
	var sphere = BABYLON.Mesh.CreateSphere("Sphere", 10.0, 3.0, scene);
	var plane = BABYLON.Mesh.CreatePlane("Plane", 50.0, scene);
	var cylinder = BABYLON.Mesh.CreateCylinder("Cylinder", 3, 3, 3, 6, scene, false);
	var torus = BABYLON.Mesh.CreateTorus("torus", 5, 1, 10, scene, false);

	box.position = new BABYLON.Vector3(-10, 0, 0);
	sphere.position = new BABYLON.Vector3(0, -10, 0);
	cylinder.position = new BABYLON.Vector3(10, 0, 0);
	torus.position = new BABYLON.Vector3(0, 10, 0);
	plane.position = new BABYLON.Vector3(0, 0, -10);
	return scene;
}