<?php
	header("Access-Control-Allow-Origin: *");
	$pId = $_POST["pId"];

	if($pId !== ""){
		if(($file = fopen("../../files/Female/last_pid.csv","w")) !== FALSE){
			if(flock($file,LOCK_EX)){
				fputcsv($file,array($pId));
			}else{
				echo "Error: Please Reload Page";
			}
			fclose($file);
		}
	}
	
?>