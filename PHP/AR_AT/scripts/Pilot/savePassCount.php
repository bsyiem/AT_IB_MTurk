<?php
	header("Access-Control-Allow-Origin: *");
	$fileName = $_POST["fileName"];
	$folderName = $_POST["folderName"];
	$passCount = $_POST["passCount"];
	$countType = $_POST["countType"];
	
	
	if(($file = fopen("../../files/Pilot/".$folderName."/".$fileName.".csv", "a")) !==FALSE){
		if(flock($file,LOCK_EX)){
			fputcsv($file, array($passCount,$countType));
		}else{
			echo "Error: Please Reload Page";
		}
		fclose($file);
	}
?>