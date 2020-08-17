<?php
	header("Access-Control-Allow-Origin: *");
	$pId = $_POST["pId"];

	if($pId !== ""){
		if(($file = fopen("../../files/Male/last_pid.csv","w")) !== FALSE){
			fputcsv($file,array($pId));
			fclose($file);
		}
	}else{
		echo "error";
	}
	
?>