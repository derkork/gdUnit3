class_name GdUnitTestSuiteDelegator
extends Node

enum {
	TYPE_CS,
	TYPE_GD
}


var _test_suite :Node
var _script_type :String
var _skipped :bool
var _active_test_case :String

func _init(test_suite :Node):
	_test_suite = test_suite
	add_child(_test_suite)
	_script_type = GdUnitScriptType.type_of(test_suite.get_script())

func set_meta(name: String, value) -> void:
	_test_suite.set_meta(name, value)

func get_script() -> Script:
	return _test_suite.get_script()

func get_name() -> String:
	return _test_suite.get_name()

# This function is called before a test suite starts
# You can overwrite to prepare test data or initalizize necessary variables
func before() -> void:
	match _script_type:
		GdUnitScriptType.GD:
			_test_suite.before()
		GdUnitScriptType.CS:
			_test_suite.Before()


# This function is called at least when a test suite is finished
# You can overwrite to cleanup data created during test running
func after() -> void:
	match _script_type:
		GdUnitScriptType.GD:
			_test_suite.after()
		GdUnitScriptType.CS:
			_test_suite.After()

# This function is called before a test case starts
# You can overwrite to prepare test case specific data
func before_test() -> void:
	match _script_type:
		GdUnitScriptType.GD:
			_test_suite.before_test()
		GdUnitScriptType.CS:
			_test_suite.BeforeTest()

# This function is called after the test case is finished
# You can overwrite to cleanup your test case specific data
func after_test() -> void:
	match _script_type:
		GdUnitScriptType.GD:
			_test_suite.after_test()
		GdUnitScriptType.CS:
			_test_suite.AfterTest()

func get_test_cases_count() -> int:
	return _test_suite.get_child_count()

func get_test_case(index :int) -> Node:
	return _test_suite.get_child(index)

func get_test_case_by_name(name :String) -> Node:
	return _test_suite.find_node(name, false, false)

func get_test_cases() -> Array:
	return _test_suite.get_children()

func delete_test_case(name :String) -> void:
	var test_case = _test_suite.find_node(name, true, false)
	_test_suite.remove_child(test_case)
	test_case.free()

# Skip the test-suite from execution, it will be ignored
func skip(skipped :bool) -> void:
	_skipped = skipped

# filters by given test names
func filter_tests(test_case_names :Array) -> void:
	for test_case in get_test_cases():
		if test_case.get_name() in test_case_names:
			continue
		_test_suite.remove_child(test_case)
		test_case.free()
		#test_case.skip(true)

func is_skipped() -> bool:
	return _skipped

func set_active_test_case(test_case :String) -> void:
	_active_test_case = test_case
	_test_suite.set_active_test_case(test_case)


# clones a test suite and moves the test cases to new instance
func clone() -> GdUnitTestSuiteDelegator:
	var test_suite = _test_suite.duplicate()
	# copy all property values
	for property in _test_suite.get_property_list():
		var property_name = property["name"]
		test_suite.set(property_name, _test_suite.get(property_name))
	
	# remove incomplete duplicated childs
	for child in test_suite.get_children():
		test_suite.remove_child(child)
		child.free()
	assert(test_suite.get_child_count() == 0)
	# now move original test cases to duplicated test suite
	for child in _test_suite.get_children():
		child.get_parent().remove_child(child)
		test_suite.add_child(child)
	# finally free current test suite instance
	remove_child(_test_suite)
	_test_suite.free()
	_test_suite = test_suite
	add_child(_test_suite)	
	return self
