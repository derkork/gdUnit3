# GdUnit generated TestSuite
class_name GdUnitToolsTest
extends GdUnitTestSuite

# TestSuite generated from
const __source = 'res://addons/gdUnit3/src/core/GdUnitTools.gd'
var file_to_save :String

# setup test data
func before():
	# opens a tmp file with WRITE mode under "user://tmp/examples/game/game.sav" (auto closed)
	var file := create_temp_file("examples/game", "game.sav")
	assert_object(file).is_not_null()
	# write some example data
	file.store_line("some data")
	# not needs to be manually close, will be auto closed before test execution

func after():
	# verify tmp files are deleted automatically
	assert_bool(File.new().file_exists(file_to_save)).is_false()

func test_create_temp_dir():
	var temp_dir := create_temp_dir("examples/game/save")
	file_to_save = temp_dir + "/save_game.dat"
	
	var data = {
		'user': "Hoschi",
		'level': 42
	}
	var file := File.new()
	file.open(file_to_save, File.WRITE)
	file.store_line(JSON.print(data))
	file.close()
	assert_bool(File.new().file_exists(file_to_save)).is_true()

func test_error_as_string():
	assert_str(error_as_string(ERR_CONNECTION_ERROR)).is_equal("Connection error.")
	assert_str(error_as_string(1000)).is_equal("Unknown error number 1000")


func test_create_temp_file():
	# opens the stored tmp file with READ mode under "user://tmp/examples/game/game.sav" (auto closed)
	var file_read := create_temp_file("examples/game", "game.sav", File.READ)
	assert_object(file_read).is_not_null()
	assert_str(file_read.get_as_text()).is_equal("some data\n")
	# not needs to be manually close, will be auto closed after test suite execution

func test_copy_directory():
	var temp_dir := create_temp_dir("test_copy_directory")
	assert_bool(GdUnitTools.copy_directory("res://addons/gdUnit3/test/core/resources/copy_test/folder_a/", temp_dir)).is_true()
	assert_file("%s/file_a.txt" % temp_dir).exists()
	assert_file("%s/file_b.txt" % temp_dir).exists()
	# TODO extends test with verify directory content assert_dir()
	
func test_copy_directory_recursive():
	var temp_dir := create_temp_dir("test_copy_directory_recursive")
	assert_bool(GdUnitTools.copy_directory("res://addons/gdUnit3/test/core/resources/copy_test/", temp_dir, true)).is_true()
	assert_file("%s/folder_a/file_a.txt" % temp_dir).exists()
	assert_file("%s/folder_a/file_b.txt" % temp_dir).exists()
	assert_file("%s/folder_b/file_a.txt" % temp_dir).exists()
	assert_file("%s/folder_b/file_b.txt" % temp_dir).exists()
	assert_file("%s/folder_b/folder_ba/file_x.txt" % temp_dir).exists()
	assert_file("%s/folder_c/file_z.txt" % temp_dir).exists()
	# TODO extends test with verify directory content assert_dir()

func test_make_qualified_path() -> void:
	assert_str(GdUnitTools.make_qualified_path("MyTest")).is_equal("MyTest")
	assert_str(GdUnitTools.make_qualified_path("/MyTest.gd")).is_equal("res://MyTest.gd")
	assert_str(GdUnitTools.make_qualified_path("/foo/bar/MyTest.gd")).is_equal("res://foo/bar/MyTest.gd")
	assert_str(GdUnitTools.make_qualified_path("res://MyTest.gd")).is_equal("res://MyTest.gd")
	assert_str(GdUnitTools.make_qualified_path("res://foo/bar/MyTest.gd")).is_equal("res://foo/bar/MyTest.gd")

func test_find_last_path_index() -> void:
	# not existing directory
	assert_int(GdUnitTools.find_last_path_index("/foo", "report_")).is_equal(0)
	# empty directory
	var temp_dir := create_temp_dir("test_reports")
	assert_int(GdUnitTools.find_last_path_index(temp_dir, "report_")).is_equal(0)
	# create some report directories
	create_temp_dir("test_reports/report_1")
	assert_int(GdUnitTools.find_last_path_index(temp_dir, "report_")).is_equal(1)
	create_temp_dir("test_reports/report_2")
	assert_int(GdUnitTools.find_last_path_index(temp_dir, "report_")).is_equal(2)
	create_temp_dir("test_reports/report_3")
	assert_int(GdUnitTools.find_last_path_index(temp_dir, "report_")).is_equal(3)
	create_temp_dir("test_reports/report_5")
	assert_int(GdUnitTools.find_last_path_index(temp_dir, "report_")).is_equal(5)
	# create some more
	for index in range(10, 42):
		create_temp_dir("test_reports/report_%d" % index)
	assert_int(GdUnitTools.find_last_path_index(temp_dir, "report_")).is_equal(41)

func test_delete_path_index_lower_equals_than() -> void:
	var temp_dir := create_temp_dir("test_reports_delete")
	assert_array(GdUnitTools.scan_dir(temp_dir)).is_empty()
	assert_int(GdUnitTools.delete_path_index_lower_equals_than(temp_dir, "report_", 0)).is_equal(0)
	
	# create some directories
	for index in range(10, 42):
		create_temp_dir("test_reports_delete/report_%d" % index)
	assert_array(GdUnitTools.scan_dir(temp_dir)).has_size(32)
	
	# try to delete directories with index lower than 0, shold delete nothing
	assert_int(GdUnitTools.delete_path_index_lower_equals_than(temp_dir, "report_", 0)).is_equal(0)
	assert_array(GdUnitTools.scan_dir(temp_dir)).has_size(32)
	
	# try to delete directories with index lower_equals than 30
	# shold delet directories report_10 to report_30 = 21
	assert_int(GdUnitTools.delete_path_index_lower_equals_than(temp_dir, "report_", 30)).is_equal(21)
	# and 12 directories are left
	assert_array(GdUnitTools.scan_dir(temp_dir))\
		.has_size(11)\
		.contains([
			"report_31",
			"report_32",
			"report_33",
			"report_34",
			"report_35",
			"report_36",
			"report_37",
			"report_38",
			"report_39",
			"report_40",
			"report_41",
		])

func test_scan_dir() -> void:
	var temp_dir := create_temp_dir("test_scan_dir")
	assert_array(GdUnitTools.scan_dir(temp_dir)).is_empty()
	
	create_temp_dir("test_scan_dir/report_2")
	assert_array(GdUnitTools.scan_dir(temp_dir)).contains_exactly(["report_2"])
	# create some more directories and files
	create_temp_dir("test_scan_dir/report_4")
	create_temp_dir("test_scan_dir/report_5")
	create_temp_dir("test_scan_dir/report_6")
	create_temp_file("test_scan_dir", "file_a")
	create_temp_file("test_scan_dir", "file_b")
	# this shoul not be counted it is a file in a subdirectory
	create_temp_file("test_scan_dir/report_6", "file_b")
	assert_array(GdUnitTools.scan_dir(temp_dir))\
		.has_size(6)\
		.contains([
			"report_2",
			"report_4",
			"report_5",
			"report_6",
			"file_a",
			"file_b"])

func execute_timer(time:float):
	yield(get_tree().create_timer(time),"timeout")

func test_is_yielded() -> void:
	assert_bool(GdUnitTools.is_yielded(null)).is_false()
	assert_bool(GdUnitTools.is_yielded("abc")).is_false()
	assert_bool(GdUnitTools.is_yielded(Resource.new())).is_false()
	assert_bool(GdUnitTools.is_yielded(1)).is_false()
	
	# start execution of a timer function with 2s
	var fs = execute_timer(2.000)
	# returnes function state is valid for 2s
	assert_bool(GdUnitTools.is_yielded(fs)).is_true()
	# wait more than 2s and the function state should be invalid
	yield(get_tree().create_timer(2.100),"timeout")
	assert_bool(GdUnitTools.is_yielded(fs)).is_false()

func _create_file(path :String, name :String) -> void:
	var file := create_temp_file(path, name)
	file.store_string("some content")
	file.close()

func test_delete_directory() -> void:
	var tmp_dir := create_temp_dir("test_delete_dir")
	create_temp_dir("test_delete_dir/data1")
	create_temp_dir("test_delete_dir/data2")
	_create_file("test_delete_dir", "example_a.txt")
	_create_file("test_delete_dir", "example_b.txt")
	_create_file("test_delete_dir/data1", "example.txt")
	_create_file("test_delete_dir/data2", "example2.txt")
	
	assert_array(GdUnitTools.scan_dir(tmp_dir)).contains_exactly_in_any_order([
		"data1",
		"data2",
		"example_a.txt",
		"example_b.txt"
	])
	
	# Delete the entire directory and its contents
	GdUnitTools.delete_directory(tmp_dir)
	assert_bool(Directory.new().dir_exists(tmp_dir)).is_false()
	assert_array(GdUnitTools.scan_dir(tmp_dir)).is_empty()

func test_delete_directory_content_only() -> void:
	var tmp_dir := create_temp_dir("test_delete_dir")
	create_temp_dir("test_delete_dir/data1")
	create_temp_dir("test_delete_dir/data2")
	_create_file("test_delete_dir", "example_a.txt")
	_create_file("test_delete_dir", "example_b.txt")
	_create_file("test_delete_dir/data1", "example.txt")
	_create_file("test_delete_dir/data2", "example2.txt")
	
	assert_array(GdUnitTools.scan_dir(tmp_dir)).contains_exactly_in_any_order([
		"data1",
		"data2",
		"example_a.txt",
		"example_b.txt"
	])
	
	# Delete the entire directory and its contents
	GdUnitTools.delete_directory(tmp_dir, true)
	assert_bool(Directory.new().dir_exists(tmp_dir)).is_true()
	assert_array(GdUnitTools.scan_dir(tmp_dir)).is_empty()

func test_extract_package() -> void:
	clean_temp_dir()
	var tmp_path := GdUnitTools.create_temp_dir("test_update")
	var source := ProjectSettings.globalize_path("res://addons/gdUnit3/test/update/resources/update.zip")
	var dest := ProjectSettings.globalize_path(tmp_path)
	
	# the temp should be inital empty
	assert_array(GdUnitTools.scan_dir(dest)).is_empty()
	# now extract to temp
	var result := GdUnitTools.extract_package(source, dest)
	assert_result(result).is_success()
	assert_array(GdUnitTools.scan_dir(dest)).contains_exactly(["MikeSchulze-gdUnit3-910d61e"])
	assert_array(GdUnitTools.scan_dir(dest + "/MikeSchulze-gdUnit3-910d61e")).contains_exactly_in_any_order([
		"addons",
		"runtest.cmd",
		"runtest.sh",
	])

func test_extract_package_invalid_package() -> void:
	clean_temp_dir()
	var tmp_path := GdUnitTools.create_temp_dir("test_update")
	var source := ProjectSettings.globalize_path("res://addons/gdUnit3/test/update/resources/update_invalid.zip")
	var dest := ProjectSettings.globalize_path(tmp_path)
	
	# the temp should be inital empty
	assert_array(GdUnitTools.scan_dir(dest)).is_empty()
	# now extract to temp
	var result := GdUnitTools.extract_package(source, dest)
	assert_result(result).is_error()\
		.contains_message("Extracting `%s` failed! Please collect the error log and report this." % source)
	assert_array(GdUnitTools.scan_dir(dest)).is_empty()


func test_find_tar_path_on_windows() -> void:
	# only execute this test on windows systems
	if OS.get_name() != "Windows":
		return
	# simulate a OS window where many tar versions are installed
	var update :GdUnitTools = mock(GdUnitTools, CALL_REAL_FUNC)
	var possible_windows_paths = PoolStringArray([
		"C:\\my_tar\\tar.exe",
		"D:\\tools\\tar.exe",
		"C:\\Windows\\System32\\tar.exe",
	])
	do_return(possible_windows_paths).on(update)._list_installed_tar_paths()
	# on windows we want to find the windows provided tar version
	assert_str(update._find_tar_path("Windows")).is_equal("C:\\Windows\\System32\\tar.exe")
	
	# Windows is installed on D:
	possible_windows_paths = PoolStringArray([
		"C:\\my_tar\\tar.exe",
		"D:\\tools\\tar.exe",
		"D:\\Windows\\System32\\tar.exe",
	])
	do_return(possible_windows_paths).on(update)._list_installed_tar_paths()
	assert_str(update._find_tar_path("Windows")).is_equal("D:\\Windows\\System32\\tar.exe")

func test_find_tar_path_on_non_windows() -> void:
	assert_str(GdUnitTools._find_tar_path("MacOS")).is_equal("tar")
	assert_str(GdUnitTools._find_tar_path("X11")).is_equal("tar")
