<?php
	header("Access-Control-Allow-Origin: *");
	$fileName = $_POST["fileName"];
	$folderName = $_POST["folderName"];
	$ledNumber = $_POST["ledNumber"];
	$reactionTime = $_POST["reactionTime"];
	$reactionType = $_POST["reactionType"];
	
	
	if(($file = fopen("../../files/Pilot/".$folderName."/".$fileName.".csv", "a")) !==FALSE){
		fputcsv($file, array($ledNumber,$reactionTime,$reactionType));		
		fclose($file);
	}else{
		echo "error";
	}
?>