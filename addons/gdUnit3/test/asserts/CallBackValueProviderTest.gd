# GdUnit generated TestSuite
#warning-ignore-all:unused_argument
#warning-ignore-all:return_value_discarded
class_name CallBackValueProviderTest
extends GdUnitTestSuite

# TestSuite generated from
const __source = 'res://addons/gdUnit3/src/asserts/CallBackValueProvider.gd'


func next_value() -> String:
	return "a value"

func test_get_value() -> void:
	var vp := CallBackValueProvider.new(self, "next_value")
	assert_str(vp.get_value()).is_equal("a value")


func test_construct_invalid() -> void:
	var vp := CallBackValueProvider.new(self, "invalid_cunc")
	assert_str(vp.get_value()).is_null()
