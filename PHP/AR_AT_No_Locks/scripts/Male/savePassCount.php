<?php
	header("Access-Control-Allow-Origin: *");
	$fileName = $_POST["fileName"];
	$folderName = $_POST["folderName"];
	$passCount = $_POST["passCount"];
	$countType = $_POST["countType"];
	
	
	if(($file = fopen("../../files/Male/".$folderName."/".$fileName.".csv", "a")) !==FALSE){
		fputcsv($file, array($passCount,$countType));
		fclose($file);
	}
?>