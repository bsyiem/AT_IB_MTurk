<?php
	header("Access-Control-Allow-Origin: *");
	$fileName = "participan_codes";
	$pid = $_POST["pid"];
	$code = $_POST["code"];
	
	if(($file = fopen("../../files/Pilot/".$fileName.".csv", "a")) !==FALSE){
		if(flock($file,LOCK_EX)){
			fputcsv($file, array($pid,$code));
		}else{
			echo "Error: Please Reload Page";
		}	
		fclose($file);
	}
?>