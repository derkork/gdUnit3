################################################################################
# This class is the main class to implement your unit tests
# You have to extend and implement your test cases as described
# e.g
# --- MyTests.gd----------------------------------------------------------------
# extends GdUnitTestSuite
#
# func test_testCaseA():
#    assert_that("value").is_equal("value")
#
#-------------------------------------------------------------------------------
# For detailed instructions show http://gdUnit/plapla
################################################################################
class_name GdUnitTestSuite
extends Node

# This function is called before a test suite starts
# You can overwrite to prepare test data or initalizize necessary variables
func before() -> void:
	pass

# This function is called at least when a test suite is finished
# You can overwrite to cleanup data created during test running
func after() -> void:
	pass

# This function is called before a test case starts
# You can overwrite to prepare test case specific data
func before_test() -> void:
	pass

# This function is called after the test case is finished
# You can overwrite to cleanup your test case specific data
func after_test() -> void:
	pass

# Skip the test-suite from execution, it will be ignored
func skip(skipped :bool) -> void:
	set_meta("gd_skipped", skipped)

func is_skipped() -> bool:
	return get_meta("gd_skipped") if has_meta("gd_skipped") else false


var __active_test_case :String
func set_active_test_case(test_case :String) -> void:
	__active_test_case = test_case

# === Tools ====================================================================
# Mapps Godot error number to a readable error message. See at ERROR
# https://docs.godotengine.org/de/stable/classes/class_@globalscope.html#enum-globalscope-error
func error_as_string(error_number :int) -> String:
	return GdUnitTools.error_as_string(error_number)

# A litle helper to auto freeing your created objects after test execution
func auto_free(obj):
	return GdUnitTools.register_auto_free(obj, get_meta(GdUnitMemoryPool.META_PARAM))

# Discard the error message triggered by a timeout (interruption).
# By default, an interrupted test is reported as an error.
# This function allows you to change the message to Success when an interrupted error is reported.
func discard_error_interupted_by_timeout() -> void:
	GdUnitTools.register_expect_interupted_by_timeout(self, __active_test_case)

# Creates a new directory under the temporary directory *user://tmp*
# Useful for storing data during test execution. 
# The directory is automatically deleted after test suite execution
func create_temp_dir(relative_path :String) -> String:
	return GdUnitTools.create_temp_dir(relative_path)

# Deletes the temporary base directory
# Is called automatically after each execution of the test suite
func clean_temp_dir():
	GdUnitTools.clear_tmp()
	
# Creates a new file under the temporary directory *user://tmp* + <relative_path>
# with given name <file_name> and given file <mode> (default = File.WRITE)
# If success the returned File is automatically closed after the execution of the test suite
func create_temp_file(relative_path :String, file_name :String, mode :=File.WRITE) -> File:
	return GdUnitTools.create_temp_file(relative_path, file_name, mode)

# Reads a resource by given path <resource_path> into a PoolStringArray.
func resource_as_array(resource_path :String) -> PoolStringArray:
	return GdUnitTools.resource_as_array(resource_path)

# Reads a resource by given path <resource_path> and returned the content as String.
func resource_as_string(resource_path :String) -> String:
	return GdUnitTools.resource_as_string(resource_path)

# Reads a resource by given path <resource_path> and return Variand translated by str2var
func resource_as_var(resource_path :String):
	return str2var(GdUnitTools.resource_as_string(resource_path))

# clears the debuger error list 
# PROTOTYPE!!!! Don't use it for now
func clear_push_errors() -> void:
	GdUnitTools.clear_push_errors()

# Creates a new scene runner to allow simulate interactions on a scene
func scene_runner(scene :Node, verbose := false) -> GdUnitSceneRunner:
	return auto_free(GdUnitSceneRunner.new(scene, verbose))

# === Mocking  & Spy ===========================================================

# do return a default value for primitive types or null 
const RETURN_DEFAULTS = GdUnitMock.RETURN_DEFAULTS
# do call the real implementation
const CALL_REAL_FUNC = GdUnitMock.CALL_REAL_FUNC
# do return a default value for primitive types and a fully mocked value for Object types
# builds full deep mocked object
const RETURN_DEEP_STUB = GdUnitMock.RETURN_DEEP_STUB

# Creates a mock for given class name
func mock(clazz, mock_mode := RETURN_DEFAULTS):
	return GdUnitMockBuilder.build(self, clazz, mock_mode)

# Creates a spy on given object instance
func spy(instance):
	return GdUnitSpyBuilder.build(self, instance)

# Configures a return value for the specified function and used arguments.
func do_return(value) -> GdUnitMock:
	return GdUnitMock.new(value)

# Verifies certain behavior happened at least once or exact number of times
func verify(obj, times := 1, expect_result :int = GdUnitAssert.EXPECT_SUCCESS):
	return GdUnitObjectInteractions.verify(obj, times, expect_result)

# Verifies no interactions is happen on this mock or spy
func verify_no_interactions(obj, expect_result :int = GdUnitAssert.EXPECT_SUCCESS) -> GdUnitAssert:
	return GdUnitObjectInteractions.verify_no_interactions(self, obj, expect_result)

# Verifies the given mock or spy has any unverified interaction.
func verify_no_more_interactions(obj, expect_result :int = GdUnitAssert.EXPECT_SUCCESS) -> GdUnitAssert:
	return GdUnitObjectInteractions.verify_no_more_interactions(self, obj, expect_result)

# Resets the saved function call counters on a mock or spy
func reset(obj) -> void:
	GdUnitObjectInteractions.reset(obj)

# === Argument matchers ========================================================
# Argument matcher to match any argument
static func any() -> GdUnitArgumentMatcher:
	return GdUnitArgumentMatchers.any()

# Argument matcher to match any boolean value
static func any_bool() -> GdUnitArgumentMatcher:
	return GdUnitArgumentMatchers.by_type(TYPE_BOOL)

# Argument matcher to match any integer value
static func any_int() -> GdUnitArgumentMatcher:
	return GdUnitArgumentMatchers.by_type(TYPE_INT)

# Argument matcher to match any float value
static func any_float() -> GdUnitArgumentMatcher:
	return GdUnitArgumentMatchers.by_type(TYPE_REAL)

# Argument matcher to match any string value
static func any_string() -> GdUnitArgumentMatcher:
	return GdUnitArgumentMatchers.by_type(TYPE_STRING)

# Argument matcher to match any Color value
static func any_color() -> GdUnitArgumentMatcher:
	return GdUnitArgumentMatchers.by_type(TYPE_COLOR)

# Argument matcher to match any Vector2 value
static func any_vector2() -> GdUnitArgumentMatcher:
	return GdUnitArgumentMatchers.by_type(TYPE_VECTOR2)

# Argument matcher to match any Vector3 value
static func any_vector3() -> GdUnitArgumentMatcher:
	return GdUnitArgumentMatchers.by_type(TYPE_VECTOR3)

# Argument matcher to match any Rect2 value
static func any_rect2() -> GdUnitArgumentMatcher:
	return GdUnitArgumentMatchers.by_type(TYPE_RECT2)

# Argument matcher to match any Plane value
static func any_plane() -> GdUnitArgumentMatcher:
	return GdUnitArgumentMatchers.by_type(TYPE_PLANE)

# Argument matcher to match any Quat value
static func any_quat() -> GdUnitArgumentMatcher:
	return GdUnitArgumentMatchers.by_type(TYPE_QUAT)

# Argument matcher to match any AABB value
static func any_aabb() -> GdUnitArgumentMatcher:
	return GdUnitArgumentMatchers.by_type(TYPE_AABB)

# Argument matcher to match any Basis value
static func any_basis() -> GdUnitArgumentMatcher:
	return GdUnitArgumentMatchers.by_type(TYPE_BASIS)

# Argument matcher to match any Transform value
static func any_transform() -> GdUnitArgumentMatcher:
	return GdUnitArgumentMatchers.by_type(TYPE_TRANSFORM)

# Argument matcher to match any Transform2D value
static func any_transform_2d() -> GdUnitArgumentMatcher:
	return GdUnitArgumentMatchers.by_type(TYPE_TRANSFORM2D)

# Argument matcher to match any NodePath value
static func any_node_path() -> GdUnitArgumentMatcher:
	return GdUnitArgumentMatchers.by_type(TYPE_NODE_PATH)

# Argument matcher to match any RID value
static func any_rid() -> GdUnitArgumentMatcher:
	return GdUnitArgumentMatchers.by_type(TYPE_RID)

# Argument matcher to match any Object value
static func any_object() -> GdUnitArgumentMatcher:
	return GdUnitArgumentMatchers.by_type(TYPE_OBJECT)

# Argument matcher to match any Dictionary value
static func any_dictionary() -> GdUnitArgumentMatcher:
	return GdUnitArgumentMatchers.by_type(TYPE_DICTIONARY)

# Argument matcher to match any Array value
static func any_array() -> GdUnitArgumentMatcher:
	return GdUnitArgumentMatchers.by_type(TYPE_ARRAY)

# Argument matcher to match any PoolByteArray value
static func any_pool_byte_array() -> GdUnitArgumentMatcher:
	return GdUnitArgumentMatchers.by_type(TYPE_RAW_ARRAY)

# Argument matcher to match any PoolIntArray value
static func any_pool_int_array() -> GdUnitArgumentMatcher:
	return GdUnitArgumentMatchers.by_type(TYPE_INT_ARRAY)

# Argument matcher to match any PoolRealArray value
static func any_pool_float_array() -> GdUnitArgumentMatcher:
	return GdUnitArgumentMatchers.by_type(TYPE_REAL_ARRAY)

# Argument matcher to match any PoolStringArray value
static func any_pool_string_array() -> GdUnitArgumentMatcher:
	return GdUnitArgumentMatchers.by_type(TYPE_STRING_ARRAY)

# Argument matcher to match any PoolVector2Array value
static func any_pool_vector2_array() -> GdUnitArgumentMatcher:
	return GdUnitArgumentMatchers.by_type(TYPE_VECTOR2_ARRAY)

# Argument matcher to match any PoolVector3Array value
static func any_pool_vector3_array() -> GdUnitArgumentMatcher:
	return GdUnitArgumentMatchers.by_type(TYPE_VECTOR3_ARRAY)

# Argument matcher to match any PoolColorArray value
static func any_pool_color_array() -> GdUnitArgumentMatcher:
	return GdUnitArgumentMatchers.by_type(TYPE_COLOR_ARRAY)

# Argument matcher to match any instance of given class
static func any_class(clazz :Object) -> GdUnitArgumentMatcher:
	return GdUnitArgumentMatchers.any_class(clazz)


# === value extract utils ======================================================
# Builds an extractor by given function name and optional arguments
static func extr(func_name :String, args := Array()) -> GdUnitValueExtractor:
	return GdUnitFuncValueExtractor.new(func_name, args)

# Constructs a tuple by given arguments
static func tuple(arg0, arg1=GdUnitTuple.NO_ARG, arg2=GdUnitTuple.NO_ARG, arg3=GdUnitTuple.NO_ARG, arg4=GdUnitTuple.NO_ARG, arg5=GdUnitTuple.NO_ARG, arg6=GdUnitTuple.NO_ARG, arg7=GdUnitTuple.NO_ARG, arg8=GdUnitTuple.NO_ARG, arg9=GdUnitTuple.NO_ARG) -> GdUnitTuple:
	return GdUnitTuple.new(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9)

# === Asserts ==================================================================
func assert_that(current, expect_result: int = GdUnitAssert.EXPECT_SUCCESS) -> GdUnitAssert:
	
	if GdObjects.is_array_type(current):
		return assert_array(current, expect_result)
	
	match typeof(current):
		TYPE_BOOL:
			return assert_bool(current, expect_result)
		TYPE_INT:
			return assert_int(current, expect_result)
		TYPE_REAL:
			return assert_float(current, expect_result)
		TYPE_STRING:
			return assert_str(current, expect_result)
		TYPE_VECTOR2:
			return assert_vector2(current, expect_result)
		TYPE_VECTOR3:
			return assert_vector3(current, expect_result)
		TYPE_DICTIONARY:
			return assert_dict(current, expect_result)
		TYPE_ARRAY:
			return assert_array(current, expect_result)
		TYPE_OBJECT:
			return assert_object(current, expect_result)
		_:
			return GdUnitAssertImpl.new(self, current, expect_result)

func assert_bool(current, expect_result: int = GdUnitAssert.EXPECT_SUCCESS) -> GdUnitBoolAssert:
	return GdUnitBoolAssertImpl.new(self, current, expect_result)

func assert_str(current, expect_result: int = GdUnitAssert.EXPECT_SUCCESS) -> GdUnitStringAssert:
	return GdUnitStringAssertImpl.new(self, current, expect_result)

func assert_int(current, expect_result: int = GdUnitAssert.EXPECT_SUCCESS) -> GdUnitIntAssert:
	return GdUnitIntAssertImpl.new(self, current, expect_result)

func assert_float(current, expect_result: int = GdUnitAssert.EXPECT_SUCCESS) -> GdUnitFloatAssert:
	return GdUnitFloatAssertImpl.new(self, current, expect_result)

func assert_vector2(current, expect_result: int = GdUnitAssert.EXPECT_SUCCESS) -> GdUnitVector2Assert:
	return GdUnitVector2AssertImpl.new(self, current, expect_result)

func assert_vector3(current, expect_result: int = GdUnitAssert.EXPECT_SUCCESS) -> GdUnitVector3Assert:
	return GdUnitVector3AssertImpl.new(self, current, expect_result)

func assert_array(current, expect_result: int = GdUnitAssert.EXPECT_SUCCESS) -> GdUnitArrayAssert:
	return GdUnitArrayAssertImpl.new(self, current, expect_result)

func assert_dict(current, expect_result: int = GdUnitAssert.EXPECT_SUCCESS) -> GdUnitDictionaryAssert:
	return GdUnitDictionaryAssertImpl.new(self, current, expect_result)

func assert_file(current, expect_result: int = GdUnitAssert.EXPECT_SUCCESS) -> GdUnitFileAssert:
	return GdUnitFileAssertImpl.new(self, current, expect_result)

func assert_object(current, expect_result: int = GdUnitAssert.EXPECT_SUCCESS) -> GdUnitObjectAssert:
	return GdUnitObjectAssertImpl.new(self, current, expect_result)

func assert_result(current, expect_result: int = GdUnitAssert.EXPECT_SUCCESS) -> GdUnitResultAssert:
	return GdUnitResultAssertImpl.new(self, current, expect_result)

func assert_not_yet_implemented():
	GdUnitAssertImpl.new(self, null).test_fail()
