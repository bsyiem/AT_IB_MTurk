<?php
	header("Access-Control-Allow-Origin: *");
	$fileName = "participan_codes";
	$pid = $_POST["pid"];
	$code = $_POST["code"];
	
	if(($file = fopen("../../files/Pilot/".$fileName.".csv", "a")) !==FALSE){
		fputcsv($file, array($pid,$code));
		fclose($file);
	}
?>